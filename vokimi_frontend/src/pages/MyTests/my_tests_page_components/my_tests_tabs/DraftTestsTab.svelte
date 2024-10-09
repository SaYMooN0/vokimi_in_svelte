<script lang="ts">
    import { PrivacyValuesUtils } from "../../../../ts/enums/PrivacyValues";
    import { TestTemplateUtils } from "../../../../ts/enums/TestTemplate";
    import { Err } from "../../../../ts/Err";
    import { getErrorFromResponse } from "../../../../ts/ErrorResponse";
    import { DraftTestBriefInfo } from "../../../../ts/my_tests_page/DraftTestBriefInfo";
    import { ImgUtils } from "../../../../ts/utils/ImgUtils";
    import DraftTestActionsMenu from "./DraftTestActionsMenu.svelte";
    import TestsTabContentWrapper from "./TestsTabContentWrapper.svelte";

    export let draftTests: DraftTestBriefInfo[] = [];
    let draftTestsActionsMenu: DraftTestActionsMenu;
    async function fetchDraftTests(skipLengthCheck: boolean): Promise<Err> {
        if (draftTests.length > 0 && !skipLengthCheck) {
            return Err.none();
        }

        const response = await fetch("/api/tests/getUserDraftTestsBriefInfo");
        if (response.status == 200) {
            const data = await response.json();
            draftTests = data.map(
                (dto: any) =>
                    new DraftTestBriefInfo(
                        dto.id,
                        dto.name,
                        dto.description,
                        dto.cover,
                        TestTemplateUtils.fromId(dto.template),
                        PrivacyValuesUtils.fromId(dto.privacy),
                    ),
            );
            return Err.none();
        } else if (response.status == 404) {
            return new Err(await getErrorFromResponse(response));
        } else {
            draftTests = [];
            return new Err("Something went wrong...");
        }
    }
</script>

<DraftTestActionsMenu bind:this={draftTestsActionsMenu} />
<TestsTabContentWrapper
    fetchTestsFunc={fetchDraftTests}
    yourTestsLabel="Your draft tests:"
>
    <div class="draft-tests-container">
        {#each draftTests as test}
            <a href="/testCreation/{test.id}/main-info-view" class="test-view">
                <div class="cover-container">
                    <img
                        class="cover"
                        src={ImgUtils.imgUrl(test.cover)}
                        alt="test cover"
                    />
                </div>
                <div class="main-info-container">
                    <label class="name">{test.name}</label>
                    <label class="desc">{test.description}</label>
                </div>
                <button
                    class="actions-btn"
                    on:click|stopPropagation|preventDefault={() =>
                        draftTestsActionsMenu.open(test.id)}
                >
                    button
                </button>
            </a>
        {/each}
    </div>
</TestsTabContentWrapper>

<style>
    .draft-tests-container {
        display: flex;
        flex-direction: column;
        gap: 8px;
    }
    .test-view {
        height: 200px;
        display: grid;
        grid-template-columns: 320px 1fr;
        position: relative;
        text-decoration: none;
        border: 2px solid transparent;
        box-shadow: rgba(99, 99, 99, 0.2) 0px 2px 6px 0px;
        border-radius: 16px;
        padding: 8px 12px;
        transition: all 0.08s;
    }
    .test-view:hover {
        border-color: var(--primary);
    }
    .test-view:active {
        border-color: var(--primary-hov);
        transform: scale(0.98);
    }
    .cover-container {
        height: inherit;
        width: 100%;
        background-color: var(--back-secondary);
        border-radius: 8px;
    }
    .cover {
        object-fit: fill;
        width: 100%;
        height: 100%;
        border-radius: 8px;
    }
    .actions-btn {
        position: absolute;
        top: 10px;
        right: 10px;
        background-color: transparent;
        border: none;
        color: var(--text-faded);
    }
    .actions-btn:hover {
        background-color: var(--back-secondary);
    }
    .actions-btn:active {
        color: var(--primary);
        transform: scale(0.98);
    }
</style>
