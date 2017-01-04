clean:
	-find -type d -name bin -exec rm -rf {} \;
	-find -type d -name obj -exec rm -rf {} \;


compile: clean 
	nuget restore PropertyConfig.sln
	xbuild /p:TargetFrameworkVersion="v4.5" /p:Configuration=Release PropertyConfig.sln

test: 
	nuget install NUnit.Runners -Version 3.5.0 -OutputDirectory tools
	mono ./tools/NUnit.Console.3.5.0/tools/nunit3-console.exe -workers 1 `(find Tests -name *Tests.dll | grep -v obj/Release)`

coverageconfig:
	chmod +x ./generateCoverageConfig.sh
	./generateCoverageConfig.sh > ./coverageConfig.json

instrument: coverageconfig
	mono ./tools/SharpCover.exe instrument ./coverageConfig.json

coverage: compile instrument test
	-mono ./tools/SharpCover.exe check
