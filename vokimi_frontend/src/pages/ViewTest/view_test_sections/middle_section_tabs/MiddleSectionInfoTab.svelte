<script lang="ts">
    import { LanguageUtils } from "../../../../ts/enums/Language";
    import { TestTemplateUtils } from "../../../../ts/enums/TestTemplate";
    import { StringUtils } from "../../../../ts/utils/StringUtils";
    import type { ViewTestBaseInfoTabData } from "../../../../ts/page_classes/view_test_page_classes/middle_section_tabs_classes/ViewTestBaseInfoTabData";
    import TagSuggestionDialog from "./info_tab_components/TagSuggestionDialog.svelte";

    export let tabData: ViewTestBaseInfoTabData;
    export let testId: string;
    let tagSuggestionDialog: TagSuggestionDialog;
</script>

<div class="test-info-tab">
    <TagSuggestionDialog bind:this={tagSuggestionDialog} {testId} />
    <div class="type-and-lang-container">
        <div class="test-type">
            Template: {TestTemplateUtils.getFullName(tabData.template)}
        </div>
        <div class="test-lang">
            Language: {LanguageUtils.getFullName(tabData.language)}
        </div>
    </div>
    {#if !StringUtils.isNullOrWhiteSpace(tabData.testDescription)}
        <div class="test-description">Description</div>
    {/if}
    <p class="test-tags-p">Test tags:</p>
    <div class="tags-container">
        {#each tabData.tags as tag}
            <div class="tag-display">#{tag}</div>
        {/each}
        <div
            class="suggest-tags-btn unselectable"
            on:click={() => tagSuggestionDialog.open()}
        >
            + Suggest tags
        </div>
    </div>
</div>

<style>
    .type-and-lang-container {
        display: grid;
        grid-template-columns: 1fr 1fr;
    }

    .type-and-lang-container div {
        display: flex;
        justify-content: center;
        align-items: center;
    }

    .test-tags-p {
        margin: 10px;
        font-size: 18px;
        color: var(--text);
    }
    .tags-container {
        margin: 10px;
        padding: 8px;
        border-radius: 4px;
        background-color: var(--back);
        display: flex;
        flex-wrap: wrap;
        gap: 8px;
    }
    .tags-container div {
        padding: 4px 8px;
        border-radius: 4px;
        cursor: pointer;
        transition: all 0.12s ease-in;
    }
    .tag-display {
        background-color: var(--primary);
        color: var(--back-main);
    }
    .suggest-tags-btn {
        background-color: var(--back-secondary);
        color: var(--text-faded);
    }
    .suggest-tags-btn:hover {
        background-color: var(--text-faded);
        color: var(--back-main);
    }
</style>
