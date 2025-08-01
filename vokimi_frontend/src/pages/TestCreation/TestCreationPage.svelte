<script lang="ts">
    import AuthorizeView from "../../components/AuthorizeView.svelte";
    import GeneralTestCreationOverview from "./general_test_creation_components/GeneralTestCreationOverview.svelte";
    import ScoringTestCreationOverview from "./scoring_test_creation_components/ScoringTestCreationOverview.svelte";
    import { Link } from "svelte-routing";
    import { TestTemplate } from "../../ts/enums/TestTemplate";
    import { ScoringTestCreationOverviewTabs } from "../../ts/page_classes/test_creation_page/test_creation_overview_tabs/scoringTestCreationOverviewTabs";
    import { GeneralTestCreationOverviewTabs } from "../../ts/page_classes/test_creation_page/test_creation_overview_tabs/generalTestCreationOverviewTabs";

    export let testId: string;

    let template: TestTemplate;
    let testName: string;
    let isCreator: boolean = false;
    let basepath = `/test-creation/${testId}`;

    async function loadTestOverviewInfo(viewerId: string | undefined) {
        if (!viewerId) {
            isCreator = false;
            return;
        }

        try {
            const response = await fetch(
                "/api/userTests/getDraftTestOverviewInfo/" + testId,
            );
            if (response.status === 200) {
                const data = await response.json();
                isCreator = data.isViewerCreator;
                template = data.template;
                testName = data.testName;
            } else {
                isCreator = false;
            }
        } catch (error) {
            isCreator = false;
        }
    }
    function updateTestName(name: string): void {
        testName = name;
    }
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
    <svelte:fragment slot="loading">
        <span>Checking Authentication</span>
    </svelte:fragment>
    <div slot="authenticated" let:authData>
        {#await loadTestOverviewInfo(authData?.UserId)}
            <p>Loading...</p>
        {:then}
            {#if isCreator}
                <p class="test-editing-label">
                    Editing of the "<label>{testName}</label>" draft test
                </p>
                <div class="tab-links-container unselectable">
                    {#each Object.entries(getTabLinks(template)) as [label, linkData]}
                        <Link to="{basepath}/{linkData.id}">
                            <div class="tab-link">
                                {#if linkData.icon}
                                    <svelte:component this={linkData.icon} />
                                {/if}
                                {label}
                            </div>
                        </Link>
                    {/each}
                </div>
                <div class="divider"></div>
                <div class="tab-content-container">
                    {#if template === TestTemplate.General}
                        <GeneralTestCreationOverview
                            {basepath}
                            {testId}
                            {updateTestName}
                        />
                    {:else if template === TestTemplate.Scoring}
                        <ScoringTestCreationOverview
                            {basepath}
                            {testId}
                            {updateTestName}
                        />
                    {/if}
                </div>
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
    .test-editing-label {
        font-size: 20px;
        width: fit-content;
        margin: 0px auto 16px auto;
        color: var(--text-faded);
        display: flex;
        align-items: center;
    }
    .test-editing-label label {
        display: inline-block;
        max-width: calc(60vw - 64px);
        text-overflow: ellipsis;
        overflow: hidden;
        white-space: nowrap;
        color: var(--text);
    }
    .tab-links-container {
        display: flex;
        justify-content: center;
        flex-direction: row;
        flex-wrap: wrap;
        padding: 0 min(12vw, 120px);
        column-gap: max(24px, calc(3.2vw - 12px));
        row-gap: 12px;
        align-items: center;
        box-sizing: border-box;
    }
    .tab-link {
        border-bottom: 2px solid transparent;
        padding-bottom: 2px;
        background-color: transparent;
        color: var(--primary);
        font-weight: 500;
        font-size: 18px;
        cursor: pointer;
        display: flex;
        flex-direction: row;
        align-items: center;
        gap: 4px;
    }
    .tab-link > :global(svg) {
        height: 24px;
        color: inherit;
    }
    .tab-link:hover {
        color: var(--primary-hov);
        border-bottom: 2px solid var(--primary-hov);
    }

    .tab-content-container {
        box-sizing: border-box;
        max-width: 1420px;
        margin: 0 auto;
    }
    .divider {
        height: 2px;
        background-color: var(--text-faded);
        opacity: 0.48;
        width: calc(100% - 24px);
        margin: 4px auto 20px auto;
        border-radius: 2px;
    }
    @media (max-width: 1200px) {
        .tab-links-container {
            padding: 0 10vw;
            column-gap: 24px;
        }
    }
    @media (max-width: 800px) {
        .tab-links-container {
            padding: 0 80px;
            column-gap: 32px;
        }
    }
</style>
