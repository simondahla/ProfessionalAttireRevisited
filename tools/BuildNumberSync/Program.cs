using System.Text.RegularExpressions;
using Mono.Cecil;

const string DefaultAssemblyPath =
    "$HOME/Library/Application Support/Steam/steamapps/common/OxygenNotIncluded/OxygenNotIncluded.app/Contents/Resources/Data/Managed/Assembly-CSharp.dll";

string assemblyPath = ExpandHome(args.Length > 0 && args[0].Length > 0 ? args[0] : DefaultAssemblyPath);
string modInfoPath = args.Length > 1 && args[1].Length > 0 ? args[1] : "mod_info.yaml";

if (!File.Exists(assemblyPath))
{
    Console.Error.WriteLine($"Assembly not found: {assemblyPath}");
    return 1;
}

if (!File.Exists(modInfoPath))
{
    Console.Error.WriteLine($"mod_info.yaml not found: {modInfoPath}");
    return 1;
}

uint changeList = ReadKleiChangeList(assemblyPath);
UpdateMinimumSupportedBuild(modInfoPath, changeList);
return 0;

static string ExpandHome(string path) =>
    path.StartsWith("$HOME", StringComparison.Ordinal)
        ? Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + path["$HOME".Length..]
        : path;

static uint ReadKleiChangeList(string assemblyPath)
{
    using var assembly = AssemblyDefinition.ReadAssembly(assemblyPath, new ReaderParameters
    {
        ReadingMode = ReadingMode.Deferred,
    });

    TypeDefinition kleiVersionType = assembly.MainModule.GetType("KleiVersion")
        ?? throw new InvalidOperationException($"KleiVersion type not found in {assemblyPath}");

    FieldDefinition changeListField = kleiVersionType.Fields.FirstOrDefault(f => f.Name == "ChangeList")
        ?? throw new InvalidOperationException("KleiVersion.ChangeList field not found");

    if (!changeListField.HasConstant || changeListField.Constant is not uint value)
    {
        throw new InvalidOperationException("KleiVersion.ChangeList is not a uint constant - game build layout may have changed");
    }

    return value;
}

static void UpdateMinimumSupportedBuild(string modInfoPath, uint changeList)
{
    string content = File.ReadAllText(modInfoPath);
    var pattern = new Regex(@"^(\s*minimumSupportedBuild\s*:\s*)(\d+)\s*$", RegexOptions.Multiline);
    Match match = pattern.Match(content);

    if (!match.Success)
    {
        throw new InvalidOperationException($"minimumSupportedBuild field not found in {modInfoPath}");
    }

    uint currentValue = uint.Parse(match.Groups[2].Value);
    if (currentValue == changeList)
    {
        Console.WriteLine($"minimumSupportedBuild already up to date ({changeList})");
        return;
    }

    File.WriteAllText(modInfoPath, pattern.Replace(content, $"${{1}}{changeList}"));
    Console.WriteLine($"minimumSupportedBuild: {currentValue} -> {changeList}");
}
