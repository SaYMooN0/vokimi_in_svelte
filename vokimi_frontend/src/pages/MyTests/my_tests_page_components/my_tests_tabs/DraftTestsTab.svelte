<script lang="ts">
    import { PrivacyValuesUtils } from "../../../../ts/enums/PrivacyValues";
    import { TestTemplateUtils } from "../../../../ts/enums/TestTemplate";
    import { Err } from "../../../../ts/Err";
    import { getErrorFromResponse } from "../../../../ts/ErrorResponse";
    import { DraftTestBriefInfo } from "../../../../ts/my_tests_page/DraftTestBriefInfo";
    import { ImgUtils } from "../../../../ts/utils/ImgUtils";
    import { StringUtils } from "../../../../ts/utils/StringUtils";
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
            <a href="/test-creation/{test.id}/main-info-view" class="test-view">
                <div class="cover-container unselectable">
                    <img
                        class="cover"
                        src={ImgUtils.imgUrl(test.cover)}
                        alt="test cover"
                    />
                </div>
                <div class="main-info-container">
                    <p class="test-name">{test.name}</p>
                    <p class="test-template">
                        Template: {TestTemplateUtils.getFullName(test.template)}
                    </p>
                    <p class="description">
                        {StringUtils.isNullOrWhiteSpace(test.description)
                            ? "Description: (None)"
                            : test.description}
                    </p>
                </div>
                <svg
                    class="actions-btn"
                    on:click|stopPropagation|preventDefault={(event) =>
                        draftTestsActionsMenu.open(test.id, event)}
                    xmlns="http://www.w3.org/2000/svg"
                    viewBox="0 0 24 24"
                    fill="none"
                >
                    <path
                        d="M11.9959 12H12.0049"
                        stroke="currentColor"
                        stroke-width="2.5"
                        stroke-linecap="round"
                        stroke-linejoin="round"
                    />
                    <path
                        d="M17.9998 12H18.0088"
                        stroke="currentColor"
                        stroke-width="2.5"
                        stroke-linecap="round"
                        stroke-linejoin="round"
                    />
                    <path
                        d="M5.99981 12H6.00879"
                        stroke="currentColor"
                        stroke-width="2.5"
                        stroke-linecap="round"
                        stroke-linejoin="round"
                    />
                </svg>
            </a>
        {/each}
    </div>
</TestsTabContentWrapper>

<style>
    .draft-tests-container {
        display: flex;
        flex-direction: column;
        gap: 16px;
    }
    .test-view {
        height: 200px;
        display: grid;
        grid-template-columns: 320px 1fr 48px;
        gap: 12px;
        position: relative;
        text-decoration: none;
        border: 2px solid transparent;
        box-shadow: rgba(99, 99, 99, 0.2) 0px 2px 6px 0px;
        border-radius: 16px;
        padding: 8px 12px;
        transition: all 0.08s;
        cursor: default;
        overflow: hidden;
    }
    .test-view:hover {
        border-color: var(--primary);
    }
    .test-view:active {
        border-color: var(--primary-hov);
        box-shadow: rgba(112, 100, 128, 0.28) 0px 3px 8px 0px;
    }
    .test-view:active .test-name {
        color: var(--primary);
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
    .test-name {
        font-size: 22px;
        font-weight: 600;
        color: var(--text);
        margin: 4px 0;
    }
    .test-template {
        margin: 4px 0;
        font-size: 18px;
        color: var(--text);
    }
    .description {
        margin: 4px 0;
        color: var(--text-faded);
        font-size: 18px;
        line-height: 1.5rem;
        overflow: hidden;
        display: -webkit-box;
        -webkit-line-clamp: 3;
        -webkit-box-orient: vertical;
    }

    .actions-btn {
        justify-self: center;
        background-color: transparent;
        border: none;
        color: var(--text-faded);
        height: 34px;
        border-radius: 8px;
    }
    .actions-btn:hover {
        background-color: var(--back-secondary);
        color: var(--primary);
    }
    .actions-btn:active {
        color: var(--primary-hov);
        transform: scale(0.98);
    }
</style>
