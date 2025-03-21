execute () {
    local path=$1

    if [ -f "$path" ];
    then
        source "$path"
    fi
}