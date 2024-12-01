<script lang="ts">
    import type { EditPageMainInfoSectionData } from "../../../ts/page_classes/profile_edit_page/EditPageMainInfoSectionData";
    import MainInfoSectionLeftSide from "./main_info_section_components/MainInfoSectionLeftSide.svelte";
    import MainInfoSectionRightSide from "./main_info_section_components/MainInfoSectionRightSide.svelte";
    import UsernameUpdateDialog from "./main_info_section_components/UsernameUpdateDialog.svelte";

    export let sectionData: EditPageMainInfoSectionData;
    let usernameUpdatingDialog: UsernameUpdateDialog;
</script>

<div class="main-info-section">
    <UsernameUpdateDialog
        bind:this={usernameUpdatingDialog}
        currentUsername={sectionData.username}
        updateParentElement={(newValue) => (sectionData.username = newValue)}
    />
    <MainInfoSectionLeftSide
        openUsernameEditingDialog={() => usernameUpdatingDialog.open()}
        username={sectionData.username}
        bannerColor={sectionData.bannerColor}
        aboutMe={sectionData.aboutMe}
    />
    <MainInfoSectionRightSide
        bind:profilePicPath={sectionData.profilePicture}
    />
</div>

<style>
    .main-info-section {
        width: 100%;
        display: grid;
        grid-template-columns: 1fr auto;
    }
</style>
