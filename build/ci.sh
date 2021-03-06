#!/bin/bash
set -e
dir="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"

# get the current git branch and set options for a non-PR analysis
branch=$(git rev-parse --abbrev-ref HEAD)
options="/d:sonar.branch.name="${branch}""

# if running in CircleCI, use the provided environment variables to set analysis options
if [ ! -z "${CIRCLECI}" ]; then
    branch="${CIRCLE_BRANCH}"
    options="/d:sonar.branch.name="${branch}""

    # if building a PR, fetch the PR info from GitHub to determine the base and branch, then set options for PR analysis
    if [ ! -z "${CIRCLE_PULL_REQUEST}" ]; then
        echo "Fetching PR to determine base and branch"
        pr_json="$(curl -s https://api.github.com/repos/jpdillingham/Soulseek.NET/pulls/${CIRCLE_PULL_REQUEST##*/})"
        base="$(echo "${pr_json}" | jq -r ".base.ref")"
        branch="$(echo "${pr_json}" | jq -r ".head.ref")"
        options="/d:sonar.pullrequest.base="${base}" /d:sonar.pullrequest.branch="${branch}" /d:sonar.pullrequest.key="${CIRCLE_PULL_REQUEST##*/}""
    fi
fi

echo "Launching dotnet-sonarscanner with options: ${options}"

# disable git bash/mingw path mangling on Windows
export MSYS2_ARG_CONV_EXCL="*"

dotnet-sonarscanner begin \
    /key:"jpdillingham_Soulseek.NET" \
    /o:jpdillingham-github \
    ${options} \
    /d:sonar.host.url="https://sonarcloud.io" \
    /d:sonar.github.repository="jpdillingham/Soulseek.NET" \
    /d:sonar.exclusions="**/*examples*/**" \
    /d:sonar.cs.opencover.reportsPaths="tests/opencover.xml" \
    /d:sonar.login="${TOKEN_SONARCLOUD}" \

. "${dir}/build.sh"
. "${dir}/test.sh"

dotnet-sonarscanner end /d:sonar.login="${TOKEN_SONARCLOUD}"

bash <(curl -s https://codecov.io/bash) -f tests/opencover.xml -t ${TOKEN_CODECOV_SOULSEEKNET}