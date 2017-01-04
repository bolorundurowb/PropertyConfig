clean:
	-find -type d -name bin -exec rm -rf {} \;
	-find -type d -name obj -exec rm -rf {} \;


compile: clean 
	mono ./nuget.exe restore PropertyConfig.sln
	xbuild /p:Configuration=Debug PropertyConfig.sln

test: 
	mono ./nuget.exe install NUnit.Runners -Version 3.5.0 -OutputDirectory tools
	mono ./tools/NUnit.ConsoleRunner.3.5.0/tools/nunit3-console.exe -workers 1 ./PropertyConfig.Tests/bin/Debug/PropertyConfig.Tests.dll

instrument:
	mono ./tools/SharpCover.exe instrument ./coverageConfig.json

coverage: compile instrument test
	-mono ./tools/SharpCover.exe check
