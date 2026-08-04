#!/bin/bash
set -euo pipefail

SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
BUILD_OUT="$SCRIPT_DIR/bin/Release/net48"
DEPLOY_DIR="${DEPLOY_DIR:-$HOME/Library/Application Support/unity.Klei.Oxygen Not Included/mods/local/ProfessionalAttireRevisited}"

echo "==> Fetching reference DLLs"
"$SCRIPT_DIR/scripts/fetch-reference-dlls.sh"

echo "==> Building (Release)"
cd "$SCRIPT_DIR"
dotnet build -c Release

echo "==> Deploying Release build to $DEPLOY_DIR"
mkdir -p "$DEPLOY_DIR"
cp "$BUILD_OUT/ProfessionalAttireRevisited.dll" "$DEPLOY_DIR/"
cp "$BUILD_OUT/ProfessionalAttireRevisited.pdb" "$DEPLOY_DIR/"
cp "$BUILD_OUT/PLib.dll" "$DEPLOY_DIR/"
cp "$SCRIPT_DIR/mod_info.yaml" "$SCRIPT_DIR/mod.yaml" "$SCRIPT_DIR/mod.info" "$SCRIPT_DIR/workshop/screenshots/preview.png" "$SCRIPT_DIR/LICENSE" "$DEPLOY_DIR/"
cp "$SCRIPT_DIR/CHANGELOG.md" "$DEPLOY_DIR/"
cp -r "$SCRIPT_DIR/translations" "$DEPLOY_DIR/"

VERSION="$(grep '^version:' "$SCRIPT_DIR/mod_info.yaml" | awk '{print $2}')"
LATEST_SECTION="$(awk '/^## /{if (found) exit; found=1} found' "$SCRIPT_DIR/CHANGELOG.md")"
# CHANGELOG.md stays Markdown for GitHub; convert this excerpt to BBCode for the Workshop template.
LATEST_SECTION="$(printf '%s\n' "$LATEST_SECTION" | awk '
  /^## / { if (in_list) { print "[/list]"; in_list=0 } next }
  /^### / { if (in_list) { print "[/list]"; in_list=0 } sub(/^### /, ""); print "[h3]" $0 "[/h3]"; next }
  /^- / { if (!in_list) { print "[list]"; in_list=1 } sub(/^- /, ""); print "[*] " $0; next }
  /^[[:space:]]*$/ { if (!in_list) print; next }
  { if (in_list) { print "[/list]"; in_list=0 } print }
  END { if (in_list) print "[/list]" }
')"

echo "==> Writing version.info (for Mod Manager, https://steamcommunity.com/sharedfiles/filedetails/?id=1843965353)"
cat > "$DEPLOY_DIR/version.info" <<EOF
<?xml version="1.0" encoding="utf-8"?>
<VersionInfo xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <Version>$VERSION.0</Version>
</VersionInfo>
EOF

echo "==> Writing RELEASE_NOTES.md from template"
TEMPLATE="$(cat "$SCRIPT_DIR/workshop/RELEASE_NOTES.template.md")"
TEMPLATE="${TEMPLATE//\{\{VERSION\}\}/$VERSION}"
TEMPLATE="${TEMPLATE//\{\{LATEST_SECTION\}\}/$LATEST_SECTION}"
printf '%s\n' "$TEMPLATE" > "$DEPLOY_DIR/RELEASE_NOTES.md"

echo "==> Done. Deployed files:"
ls -la "$DEPLOY_DIR"
echo ""
echo "Steam Workshop upload is manual - Klei's Steam Mod Uploader app reads directly from"
echo "this folder. Open it, check \"Update Data\", point it at:"
echo "  $DEPLOY_DIR"
echo "and paste this version's notes from CHANGELOG.md into the changenote field."
