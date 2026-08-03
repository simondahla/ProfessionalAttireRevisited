#!/bin/bash
set -euo pipefail

SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
MOD_STATIC_ID="SimonDahla.ProfessionalAttireRevisited"
DEPLOY_DIR="$HOME/Library/Application Support/unity.Klei.Oxygen Not Included/mods/local/ProfessionalAttireRevisited"
MODS_JSON="$HOME/Library/Application Support/unity.Klei.Oxygen Not Included/mods/mods.json"
BUILD_OUT="$SCRIPT_DIR/bin/Debug/net48"
ONI_STEAM_APPID="457140"
QUIT_TIMEOUT_SECONDS=30
DEBUG_ENABLE_FILE="$HOME/Library/Application Support/Steam/steamapps/common/OxygenNotIncluded/OxygenNotIncluded.app/Contents/debug_enable.txt"

if pgrep -f "Oxygen Not Included" > /dev/null; then
  echo "==> ONI is running, quitting it"
  osascript -e 'quit app "Oxygen Not Included"' || true
  waited=0
  while pgrep -f "Oxygen Not Included" > /dev/null; do
    if [ "$waited" -ge "$QUIT_TIMEOUT_SECONDS" ]; then
      echo "ONI didn't quit within ${QUIT_TIMEOUT_SECONDS}s (a save/quit dialog may be blocking it) - quit it manually and rerun."
      exit 1
    fi
    sleep 1
    waited=$((waited + 1))
  done
  echo "==> ONI quit cleanly"
fi

"$SCRIPT_DIR/build.sh"

echo "==> Deploying to $DEPLOY_DIR"
mkdir -p "$DEPLOY_DIR"
cp "$BUILD_OUT/ProfessionalAttireRevisited.dll" "$DEPLOY_DIR/"
cp "$BUILD_OUT/ProfessionalAttireRevisited.pdb" "$DEPLOY_DIR/"
cp "$BUILD_OUT/PLib.dll" "$DEPLOY_DIR/"
cp "$SCRIPT_DIR/mod_info.yaml" "$DEPLOY_DIR/"
cp "$SCRIPT_DIR/mod.yaml" "$DEPLOY_DIR/"
cp "$SCRIPT_DIR/preview.png" "$DEPLOY_DIR/"

echo "==> Ensuring mod is enabled in mods.json"
tmp="$(mktemp)"
jq --arg id "$MOD_STATIC_ID" '(.mods[] | select(.staticID == $id) | .enabled) = true' "$MODS_JSON" > "$tmp"
mv "$tmp" "$MODS_JSON"

echo "==> Resetting debug_enable.txt"
rm -f "$DEBUG_ENABLE_FILE"
touch "$DEBUG_ENABLE_FILE"

echo "==> Launching ONI via Steam"
open "steam://run/$ONI_STEAM_APPID"

echo "==> Spawning background watcher to clear debug_enable.txt once ONI quits"
(
  while ! pgrep -f "Oxygen Not Included" > /dev/null; do
    sleep 2
  done
  while pgrep -f "Oxygen Not Included" > /dev/null; do
    sleep 2
  done
  rm -f "$DEBUG_ENABLE_FILE"
) &
disown

echo "==> Done. Click Resume/Continue in the main menu once it's up."
