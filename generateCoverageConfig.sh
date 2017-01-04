ASSEMBLIES=`find . -type f -name PropertyConfig*.dll | grep -v /obj/ | grep -v *Tests.dll | perl -e '@in=grep(s/\n$//, <>); print "[\"".join("\", \"", @in)."\"],\n";'`
echo "{"
echo "  \"assemblies\": ${ASSEMBLIES}" 
echo "  \"typeInclude\": \"PropertyConfig.*\"," 
echo "  \"typeExclude\": \"PropertyConfig.*Tests*\""
echo "}"