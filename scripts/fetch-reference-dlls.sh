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
fetch_and_verify "Newtonsoft.Json.dll" "e7fdfb49e6140f83ff1fbf4893cc25ad1c1669e72df470c2d284aaa593fe70cf"
fetch_and_verify "0Harmony.dll" "bbf485c419484fd1637e1a7c5bc282a62c63bf2f90571ff0dedc1de584fbfaf8"
fetch_and_verify "UnityEngine.UI.dll" "b6046afbdca4b74bc5da57b781d341ad23631659ffac6d634666bc48239120c2"
fetch_and_verify "UnityEngine.UIModule.dll" "44e9e848f203e02f65cce5b5557a296b7a1bd0227f469ebb7b43a194744a5b56"
fetch_and_verify "UnityEngine.dll" "89095f7c9f40594c6940584c7f6a4aed843f33163dbba7af8302a23c9e710baa"
fetch_and_verify "Unity.TextMeshPro.dll" "259bb3103d03cb77667f91e46d1022dbf131bd2ad93eb7becd6a61cc7686dda9"
fetch_and_verify "UnityEngine.TextRenderingModule.dll" "8590e9a5d4dd726924b1fa8a3da4b12167b288b9347f30e36479a1793fb2da94"

# ILRepack resolves assemblies by their real names (no _public suffix).
ln -sf Assembly-CSharp_public.dll "$LIB_DIR/Assembly-CSharp.dll"
ln -sf Assembly-CSharp-firstpass_public.dll "$LIB_DIR/Assembly-CSharp-firstpass.dll"

echo "==> All reference DLLs fetched and verified"
