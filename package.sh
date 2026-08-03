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

echo "==> Done. Deployed files:"
ls -la "$DEPLOY_DIR"
echo ""
echo "Steam Workshop upload is manual - Klei's Steam Mod Uploader app reads directly from"
echo "this folder. Open it, check \"Update Data\", point it at:"
echo "  $DEPLOY_DIR"
echo "and paste this version's notes from CHANGELOG.md into the changenote field."
