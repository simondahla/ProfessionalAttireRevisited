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

echo "==> Writing RELEASE_NOTES.md"
VERSION="$(grep '^version:' "$SCRIPT_DIR/mod_info.yaml" | awk '{print $2}')"
LATEST_SECTION="$(awk '/^## /{if (found) exit; found=1} found' "$SCRIPT_DIR/CHANGELOG.md")"
cat > "$DEPLOY_DIR/RELEASE_NOTES.md" <<EOF
# Professional Attire Revisited - v$VERSION

## What's new

$LATEST_SECTION

## Found a bug?

Please report it: https://github.com/simondahla/ProfessionalAttireRevisited/issues/new

When reporting, please include:

- **What happened:**
- **What you expected to happen:**
- **Steps to reproduce:**
- **Other mods enabled:**
- **Save file (if relevant):** attach or link
- **Game version:**
EOF

echo "==> Done. Deployed files:"
ls -la "$DEPLOY_DIR"
echo ""
echo "Steam Workshop upload is manual - Klei's Steam Mod Uploader app reads directly from"
echo "this folder. Open it, check \"Update Data\", point it at:"
echo "  $DEPLOY_DIR"
echo "and paste this version's notes from CHANGELOG.md into the changenote field."
