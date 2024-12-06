<script lang="ts">
    import { EditPageLinksSectionData } from "../../../ts/page_classes/profile_edit_page/EditPageLinksSectionData";
    import { StringUtils } from "../../../ts/utils/StringUtils";
    import SectionHeader from "../section_shared_components/SectionHeader.svelte";
    import SectionsEditButtons from "../section_shared_components/SectionsEditButtons.svelte";
    import LinksUpdatingDialog from "./links_section_components/LinksUpdatingDialog.svelte";

    export let sectionData: EditPageLinksSectionData;
    let linksUpdatingDialog: LinksUpdatingDialog;
</script>

<LinksUpdatingDialog
    bind:this={linksUpdatingDialog}
    updateParentElement={(newVal) => {
        sectionData = newVal;
    }}
    currentLinksData={sectionData}
/>
<SectionHeader headerText="My links:" />
<div class="links-container">
    {#each sectionData.toNameValList() as { linkName, linkVal }}
        <span class="link-name">{linkName}: </span>
        {#if StringUtils.isNullOrWhiteSpace(linkVal)}
            <span class="link-not-set">(Not set)</span>
        {:else}
            <a href={linkVal} class="link-value">{linkVal}</a>
        {/if}
    {/each}
</div>
<SectionsEditButtons
    editButtonAction={() => linksUpdatingDialog.open()}
    text="Edit links"
/>

<style>
    .links-container {
        display: grid;
        grid-template-columns: auto 1fr;
        align-items: center;
        padding: 0;
        row-gap: 8px;
        column-gap: 16px;
        color: var(--text);
    }

    .link-name {
        color: inherit;
        font-size: 22px;
    }
    .link-value {
        color: inherit;
        font-size: 20px;
        text-decoration: underline;
        padding: 2px 6px;
        border-radius: 2px;
        width: fit-content;
    }
    .link-value:hover {
        color: var(--primary);
    }
    .link-value:active {
        background-color: var(--back-secondary);
        color: var(--primary-hov);
    }

    .link-not-set {
        font-size: 20px;
        color: var(--text-faded);
    }
</style>
