#!/bin/bash

pushd src/Test1.ApiService

dotnet stryker \
  --test-project ../Test1.ApiService.Tests.Unit/Test1.ApiService.Tests.Unit.csproj \
  --config-file ../../stryker-config.json

popd
