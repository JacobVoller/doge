deploy_components () {
    src_dir=./src/components
    import_file=./bin/_sass/components.scss

    for path in "$src_dir"/*
    do
        component_name="$(basename $path)"

        if [[ $component_name != _* ]]
        then
            # PLUGIN
            source=$src_dir/$component_name/plugin.rb
            target=./bin/_plugins/z_components/$component_name.rb
            copy_file $source $target
            
            # SASS
            source=$src_dir/$component_name/style.scss

            if [ -f $source ]
            then
                echo "@import \"components/$component_name\";" >> $import_file
            fi

            target=./bin/_sass/components/$component_name.scss
            copy_file $source $target
        fi
    done
}

# CLEAN
echo "      > [WEB] Clean"
dir_delete ./docs # delete existing static files

# BUILD
echo "      > [WEB] Build"
cd apps/web/src/jekyll
gem install jekyll bundler
bundle update
jekyll build --config _config.yml --quiet

# STATIC
echo "      > [WEB] Deploy Static"
cd ../../
dir_copy static ../../../docs

# WRAP UP
cd ../../
echo "      > [WEB] Done"