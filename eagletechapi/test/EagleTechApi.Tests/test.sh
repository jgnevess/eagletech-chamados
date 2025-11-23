#!/bin/bash

function test() {
    clear
    rm -rf obj bin
    dotnet test
    rm -rf obj bin
}

function testWithArgs() {
    dotnet test --filter "FullyQualifiedName~EagleTechApi.Tests.$1" \
        --logger "console;verbosity=normal" \
        --verbosity detailed
}
