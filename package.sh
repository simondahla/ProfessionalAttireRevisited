#!/bin/bash
set -euo pipefail

SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
BUILD_OUT="$SCRIPT_DIR/bin/Release/net48"
DEPLOY_DIR="$HOME/Library/Application Support/unity.Klei.Oxygen Not Included/mods/local/ProfessionalAttireRevisited"

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
cp "$SCRIPT_DIR/mod_info.yaml" "$SCRIPT_DIR/mod.yaml" "$SCRIPT_DIR/preview.png" "$DEPLOY_DIR/"
cp "$SCRIPT_DIR/CHANGELOG.md" "$DEPLOY_DIR/"

VERSION="$(grep '^version:' "$SCRIPT_DIR/mod_info.yaml" | awk '{print $2}')"
LATEST_SECTION="$(awk '/^## /{if (found) exit; found=1} found' "$SCRIPT_DIR/CHANGELOG.md")"

echo "==> Writing version.info and mod.info (for Mod Manager, https://steamcommunity.com/sharedfiles/filedetails/?id=1843965353)"
cat > "$DEPLOY_DIR/version.info" <<EOF
<?xml version="1.0" encoding="utf-8"?>
<VersionInfo xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <Version>$VERSION.0</Version>
</VersionInfo>
EOF
cat > "$DEPLOY_DIR/mod.info" <<EOF
<?xml version="1.0" encoding="utf-8"?>
<ModInfoData xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <Github>https://github.com/simondahla/ProfessionalAttireRevisited</Github>
</ModInfoData>
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
