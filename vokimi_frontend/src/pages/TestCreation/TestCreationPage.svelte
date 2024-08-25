<script lang="ts">
    import { onMount } from "svelte";
    import { TestTemplate } from "../../ts/enums/TestTemplate";
    import AuthorizeView from "../../components/AuthorizeView.svelte";
    import GeneralTestCreationOverview from "./general_test_creation_components/GeneralTestCreationOverview.svelte";
    import GeneralTestCreationOverviewTabs from "./general_test_creation_components/GeneralTestCreationOverview.svelte";
    import { Link } from "svelte-routing";

    export let testId: string;

    let template: TestTemplate;
    let basepath = "/test-creation/:testId";
    let testName: string;

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
        const url = "/api/getDraftTestTemplate/" + testId;
        const response = await fetch(url);
        if (response.status == 200) {
            let j = await response.json();
            template = j.template;
        }
    });

    function getTabLinks() {
        switch (template) {
            case TestTemplate.General:
                return GeneralTestCreationOverviewTabs;
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
                    {#each Object.entries(getTabLinks()) as [key, label]}
                        <Link to="{basepath}/{key}">{label}</Link>
                    {/each}
                </div>
                {#if template === TestTemplate.General}
                    <GeneralTestCreationOverview {basepath} />
                {:else if template === TestTemplate.Scoring}
                    <!-- <ScoringTestCreationOverview  {basepath}/> -->
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
