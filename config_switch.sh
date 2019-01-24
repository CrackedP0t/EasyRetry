#!/bin/bash

if [[ $1 == "" ]]; then
    echo "Need configuration as argument"
    exit 1
fi

MODPATH=~/.steam/steam/steamapps/common/Celeste/Mods/EasyRetry

[[ -e $MODPATH ]] && rm $MODPATH

ln -s $(pwd)/EasyRetry/bin/$1/ $MODPATH
