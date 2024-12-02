<script lang="ts">
    import type { EditPageMainInfoSectionData } from "../../../ts/page_classes/profile_edit_page/EditPageMainInfoSectionData";
    import MainInfoSectionLeftSide from "./main_info_section_components/MainInfoSectionLeftSide.svelte";
    import MainInfoSectionRightSide from "./main_info_section_components/MainInfoSectionRightSide.svelte";
    import AboutMeUpdateDialog from "./main_info_section_components/left_side_components/AboutMeUpdateDialog.svelte";
    import BannerColorUpdateDialog from "./main_info_section_components/left_side_components/BannerColorUpdateDialog.svelte";
    import UsernameUpdateDialog from "./main_info_section_components/left_side_components/UsernameUpdateDialog.svelte";

    export let sectionData: EditPageMainInfoSectionData;
    let usernameUpdatingDialog: UsernameUpdateDialog;
    let aboutMeUpdatingDialog: AboutMeUpdateDialog;
    let bannerColorUpdatingDialog: BannerColorUpdateDialog;
</script>

<div class="main-info-section">
    <UsernameUpdateDialog
        bind:this={usernameUpdatingDialog}
        currentUsername={sectionData.username}
        updateParentElement={(newValue) => (sectionData.username = newValue)}
    />
    <AboutMeUpdateDialog
        bind:this={aboutMeUpdatingDialog}
        currentAboutMe={sectionData.aboutMe}
        updateParentElement={(newValue) => (sectionData.aboutMe = newValue)}
    />
    <BannerColorUpdateDialog
        bind:this={bannerColorUpdatingDialog}
        currentBannerColor={sectionData.bannerColor}
        updateParentElement={(newValue) => (sectionData.bannerColor = newValue)}
    />
    <MainInfoSectionLeftSide
        openUsernameEditingDialog={() => usernameUpdatingDialog.open()}
        openAboutMeEditingDialog={() => aboutMeUpdatingDialog.open()}
        openBannerColorEditingDialog={() => bannerColorUpdatingDialog.open()}
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
