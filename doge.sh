#!/usr/bin/env bash

set -e # stop script execution on failure

clear
echo "[DOGE] -- START"

DIR_APPS="apps"
DIR_BUILD="build"
PATH_BUILD_ENTRY="$DIR_APPS/$DIR_BUILD/src/entry.sh"

source "$PATH_BUILD_ENTRY"

echo "[DOGE] -- END"