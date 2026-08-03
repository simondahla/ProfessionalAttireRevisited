#!/bin/bash
set -euo pipefail

SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
LIB_DIR="$SCRIPT_DIR/../lib"
PIN_SHA="101a31912ab277778fd9b7a9cfc5a419995e1084"
RAW_BASE="https://raw.githubusercontent.com/peterhaneve/ONIMods/$PIN_SHA/Lib"

mkdir -p "$LIB_DIR"

fetch_and_verify() {
  local name="$1" expected="$2" dest="$LIB_DIR/$1"
  echo "==> Fetching $name"
  curl -fsSL "$RAW_BASE/$name" -o "$dest"
  local actual
  actual="$(shasum -a 256 "$dest" | awk '{print $1}')"
  if [ "$actual" != "$expected" ]; then
    echo "Checksum mismatch for $name: expected $expected, got $actual" >&2
    exit 1
  fi
}

fetch_and_verify "Assembly-CSharp_public.dll" "b68aad746d3152a4242d2204d799b13b2ca35b0149ffdef4dcbfd17263f7515f"
fetch_and_verify "Assembly-CSharp-firstpass_public.dll" "bbf298bd85ff5f8b52569a289fe3ba5ffbff159d9a014466c738f3607b0f3783"
fetch_and_verify "UnityEngine.CoreModule.dll" "81c7b8183600ff8d2a36cdadbd73555e75d0b4f99c4d8851bc7be929de786ab1"

echo "==> All reference DLLs fetched and verified"
