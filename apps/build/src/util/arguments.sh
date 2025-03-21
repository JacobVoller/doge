function argumentExists {
    local search_string="$1"
    
    for arg in $ARGS; 
    do
        if [[ "$arg" == "$search_string" ]]; 
        then
            echo 1
            return
        fi
    done
    
    echo 0
}