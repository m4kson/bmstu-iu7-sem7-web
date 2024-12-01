PATH_TO_TEST=$1;
echo "$1"
dotnet test $PATH_TO_TEST -filter TestCategory=IntegrationTest --logger:"console;verbosity=detailed" --timeout 600000
