<script lang="ts">
    import { onMount } from "svelte";
    import { TestTemplate } from "../../ts/enums/TestTemplate";
    import AuthorizeView from "../../components/AuthorizeView.svelte";
    import GeneralTestCreationOverview from "./general_test_creation_components/GeneralTestCreationOverview.svelte";
    import { GeneralTestCreationOverviewTabs } from "./general_test_creation_components/generalTestCreationOverviewTabs";
    import ScoringTestCreationOverview from "./scoring_test_creation_components/ScoringTestCreationOverview.svelte";
    import { ScoringTestCreationOverviewTabs } from "./scoring_test_creation_components/generalTestCreationOverviewTabs";
    import { Link } from "svelte-routing";

    export let testId: string;

    let template: TestTemplate;
    let testName: string;
    let basepath = "/test-creation/:testId";

    async function checkIfViewerIsCreator(
        userId: string | undefined,
    ): Promise<boolean> {
        if (userId === undefined) {
            return false;
        }
        const data = { testId, userId };
        const response = await fetch("/api/checkIfUserIsDraftTestCreator", {
            method: "Post",
            headers: {
                "Content-Type": "application/json",
            },
            body: JSON.stringify(data),
        });
        return response.status == 200;
    }
    onMount(async () => {
        const url = "/api/getDraftTestTemplateWithName/" + testId;
        const response = await fetch(url);
        if (response.status == 200) {
            let j = await response.json();
            template = j.template;
            testName = j.name;
        }
    });

    function getTabLinks(t: TestTemplate) {
        switch (t) {
            case TestTemplate.General:
                return GeneralTestCreationOverviewTabs;
            case TestTemplate.Scoring:
                return ScoringTestCreationOverviewTabs;
            default:
                throw new Error("Template not implemented");
        }
    }
</script>

<AuthorizeView>
    <div slot="loading">
        <span>Checking Authentication</span>
    </div>
    <div slot="authenticated" let:authData>
        {#await checkIfViewerIsCreator(authData.UserId)}
            <p>Loading...</p>
        {:then isCreator}
            {#if isCreator}
                <p class="test-editing-label">
                    <span>Editing of the "</span>
                    <span class="test-name">{testName}</span>
                    <span>" draft test</span>
                </p>
                <div class="tab-links-container">
                    {#each Object.entries(getTabLinks(template)) as [label, path]}
                        <Link to="/test-creation/{testId}/{path}">
                            <div class="tab-link">
                                {label}
                            </div>
                        </Link>
                    {/each}
                </div>
                {#if template === TestTemplate.General}
                    <GeneralTestCreationOverview {basepath} />
                {:else if template === TestTemplate.Scoring}
                    <ScoringTestCreationOverview {basepath} />
                {/if}
            {:else}
                <div class="no-access-div">
                    You don't have access to this test
                </div>
            {/if}
        {/await}
    </div>
    <div slot="unauthenticated">
        <p>You have to be authenticated to access this page</p>
    </div>
</AuthorizeView>

<style>
    .tab-links-container {
        display: flex;
        justify-content: center;
        gap: 2vw;
        align-items: center;
        box-sizing: border-box;
    }
    .tab-link {
        padding: 10px 20px;
        border-radius: 5px;
        background-color: transparent;
        color: var(--primary);
        font-weight: 500;
        font-size: 18px;
        cursor: pointer;
    }
    .tab-link:hover {
        background-color: var(--back-secondary);
        color: var(--primary-hov);
    }
</style>
