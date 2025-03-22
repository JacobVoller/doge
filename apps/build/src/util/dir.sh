dir_delete () {
    local path=$1

    rm -rf $path
}

dir_create () {
    local path=$1
    
    if [ ! -d $path ]
    then
        mkdir -p $path
    fi
}

dir_copy () {
    local source=$1
    local target=$2

    if [ -d $source ]
    then
        dir_create $target
        cp -a $source/. $target/
    fi
}