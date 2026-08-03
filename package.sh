#!/bin/bash
set -euo pipefail

SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
BUILD_OUT="$SCRIPT_DIR/bin/Release/net48"
PACKAGE_DIR="$SCRIPT_DIR/package"

echo "==> Fetching reference DLLs"
"$SCRIPT_DIR/scripts/fetch-reference-dlls.sh"

echo "==> Building (Release)"
cd "$SCRIPT_DIR"
dotnet build -c Release

echo "==> Assembling $PACKAGE_DIR"
rm -rf "$PACKAGE_DIR"
mkdir -p "$PACKAGE_DIR"
cp "$BUILD_OUT/ProfessionalAttireRevisited.dll" "$PACKAGE_DIR/"
cp "$BUILD_OUT/ProfessionalAttireRevisited.pdb" "$PACKAGE_DIR/"
cp "$BUILD_OUT/PLib.dll" "$PACKAGE_DIR/"
cp "$SCRIPT_DIR/mod_info.yaml" "$SCRIPT_DIR/mod.yaml" "$SCRIPT_DIR/preview.png" "$PACKAGE_DIR/"

echo "==> Done. Package contents:"
ls -la "$PACKAGE_DIR"
echo ""
echo "To test a real upload locally, run steamcmd against build.vdf directly"
echo "(its contentfolder/previewfile already point at $PACKAGE_DIR):"
echo "  ./steamcmd.sh +login <user> +workshop_build_item build.vdf +quit"
