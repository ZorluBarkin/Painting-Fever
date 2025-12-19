#!/bin/bash
set -e

# --- Color Definitions ---
RED='\033[0;31m'
GREEN='\033[0;32m'
YELLOW='\033[0;33m'
CYAN='\033[0;36m'
NC='\033[0m' # No Color (Reset)

# 1. Check if we are on the 'main' branch
CURRENT_BRANCH=$(git rev-parse --abbrev-ref HEAD)
if [ "$CURRENT_BRANCH" != "main" ]; then
    echo -e "${RED}Error: You must be on the 'main' branch to release. (Currently on '$CURRENT_BRANCH')${NC}"
    exit 1
fi

# 2. Check for uncommitted changes
if ! git diff-index --quiet HEAD --; then
    echo -e "${RED}Error: You have uncommitted changes. Please commit or stash them first.${NC}"
    exit 1
fi

# 3. Get the version argument
INPUT_VERSION=$1

# 4. Ultra-Strict Regex check for X.X.X format
if [[ ! $INPUT_VERSION =~ ^(0|[1-9][0-9]?)\.(0|[1-9][0-9]?)\.(0|[1-9][0-9]?)$ ]]; then
    echo -e "${RED}Error: Invalid version format '$INPUT_VERSION'.${NC}"
    echo -e "${YELLOW}Rules: X.X.X format. Use '0.1.0', not '0.01.0'. No leading zeros. Max 2 digits per section.${NC}"
    exit 1
fi

# 5. Add the 'v' prefix for the Git Tag
VERSION="v$INPUT_VERSION"

echo -e "${CYAN}Starting release process for $VERSION...${NC}"

# 6. Execute the sync
git checkout release --quiet
git merge --squash main
git commit -m "Release $VERSION"
git tag -a "$VERSION" -m "Version $VERSION"
git push origin release --follow-tags

# 7. Return to main
git checkout main --quiet

echo -e "${GREEN}Successfully synced main to release as $VERSION!${NC}"
