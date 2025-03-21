import_utils () {
    local util_dir="$DIR_APPS/$DIR_BUILD/src/util"
    local files=($util_dir/*)

    for file in "${files[@]}";
    do
        if [ -f "$file" ];
        then
            source "$file"
        fi
    done
}

execute_app_script () {
    local app_dir=$1
    local script_name=$2
    local path="$DIR_APPS/$app_dir/scripts/$script_name.sh"

    execute "$path" 2>/dev/null
}

# SETUP
ARGS=$@
import_utils
LAUNCH=$(argumentExists "-launch")

# CLEAN

# BUILD
execute_app_script "db" "build" #2>/dev/null
execute_app_script "server" "build" #2>/dev/null
execute_app_script "web" "build" #2>/dev/null

# LAUNCH
if [[ LAUNCH -eq 1 ]]
then
    execute_app_script "db" "launch" 2>/dev/null
fi