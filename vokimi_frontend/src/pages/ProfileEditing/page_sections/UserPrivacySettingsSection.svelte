<script lang="ts">
    import type { EditPagePrivacySettingsSectionData } from "../../../ts/page_classes/profile_edit_page/EditPagePrivacySettingsSectionData";
    import SectionHeader from "../section_shared_components/SectionHeader.svelte";

    export let sectionData: EditPagePrivacySettingsSectionData;
</script>

<SectionHeader headerText="Privacy settings:" />
<div class="privacy-divs-container">
    {#each Object.entries(sectionData) as privacy}
        <div>{privacy[0]} {privacy[1]}</div>
    {/each}
</div>
