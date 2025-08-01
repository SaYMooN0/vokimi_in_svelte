<script lang="ts">
    import { Err } from "../../../ts/Err";
    import { getErrorFromResponse } from "../../../ts/ErrorResponse";
    import { ManageTestTagsTabData } from "../../../ts/page_classes/manage_test_page/tags/ManageTestTagsTabData";
    import { TagSuggestionForTest } from "../../../ts/page_classes/manage_test_page/tags/TagSuggestionForTest";
    import { StringUtils } from "../../../ts/utils/StringUtils";
    import TabContentWrapper from "../page_layout/TabContentWrapper.svelte";
    import TabSubHeader from "../tabs_shared/TabSubHeader.svelte";
    import CurrentTestTagsView from "./CurrentTestTagsView.svelte";
    import TagsSuggestionsView from "./TagsSuggestionsView.svelte";
    export let testId: string;
    export let isActive: boolean;

    let tabData: ManageTestTagsTabData;
    let suggestionsAbilityChangeError: string = "";
    async function fetchTabData(): Promise<Err> {
        const response = await fetch(`/api/manageTest/tags/tabData/${testId}`);
        if (response.ok) {
            const data = await response.json();
            tabData = ManageTestTagsTabData.fromResponseData(data);
            return Err.none();
        } else if (response.status === 400) {
            return new Err(await getErrorFromResponse(response));
        } else {
            return new Err("Something went wrong.");
        }
    }
    async function enableTagsSuggestions() {
        suggestionsAbilityChangeError = "";
        const response = await fetch(
            `/api/manageTest/tags/enableTestTagsSuggestions/${testId}`,
            { method: "POST" },
        );
        if (response.ok) {
            const data = await response.json();
            tabData.tagsSuggestions = data.tagsSuggestions;
            tabData.tagsSuggestionsAllowed = data.tagsSuggestionsAllowed;
        } else if (response.status === 400) {
            suggestionsAbilityChangeError =
                await getErrorFromResponse(response);
        } else {
            suggestionsAbilityChangeError =
                "Failed to enable suggestions. Try again later.";
        }
    }
    async function disableTagsSuggestions() {
        suggestionsAbilityChangeError = "";
        const response = await fetch(
            `/api/manageTest/tags/disableTestTagsSuggestions/${testId}`,
            { method: "POST" },
        );
        if (response.ok) {
            const data = await response.json();
            tabData.tagsSuggestions = [];
            tabData.tagsSuggestionsAllowed = data.tagsSuggestionsAllowed;
        } else if (response.status === 400) {
            suggestionsAbilityChangeError =
                await getErrorFromResponse(response);
        } else {
            suggestionsAbilityChangeError =
                "Failed to disable suggestions. Try again later.";
        }
    }
</script>

<TabContentWrapper {fetchTabData} {isActive} let:updateTabData>
    <CurrentTestTagsView
        maxTagsCount={tabData.maxTagsForTestCount}
        tags={tabData.testTags}
        {testId}
        updateParentElement={updateTabData}
    />

    <TabSubHeader headerText="Tags suggestions" />
    {#if !tabData.tagsSuggestionsAllowed}
        <p class="suggestions-message">
            Tags suggestions for this test are disabled
            <button
                class="enable-suggestions-btn"
                on:click={() => enableTagsSuggestions()}
            >
                Enable Tags suggestions
            </button>
        </p>
    {:else}
        <p class="suggestions-message">
            Tags suggestions for this test are enabled
            <button
                class="disable-suggestions-btn"
                on:click={() => disableTagsSuggestions()}
            >
                Disable Tags suggestions
            </button>
        </p>
    {/if}

    {#if !StringUtils.isNullOrWhiteSpace(suggestionsAbilityChangeError)}
        <p class="suggestions-allowed-change-error">
            {suggestionsAbilityChangeError}
        </p>
    {/if}
    {#if tabData.tagsSuggestionsAllowed}
        <TagsSuggestionsView
            bind:tagsSuggestions={tabData.tagsSuggestions}
            addNewTag={(newTagValue) => {
                tabData.testTags = [...tabData.testTags, newTagValue];
            }}
            {testId}
        />
    {/if}
</TabContentWrapper>

<style>
    .suggestions-allowed-change-error {
        color: var(--red-del);
        font-weight: 400;
        font-size: 16px;
        margin: 4px 0;
    }
</style>
