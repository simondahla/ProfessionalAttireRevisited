#!/bin/bash
set -euo pipefail

SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"

echo "==> Syncing minimumSupportedBuild from installed game"
dotnet run --project "$SCRIPT_DIR/tools/BuildNumberSync" -- "" "$SCRIPT_DIR/mod_info.yaml"

echo "==> Building"
cd "$SCRIPT_DIR"
dotnet build
