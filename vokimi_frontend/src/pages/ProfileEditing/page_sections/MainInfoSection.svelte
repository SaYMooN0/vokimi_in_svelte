<script lang="ts">
    import type { EditPageMainInfoSectionData } from "../../../ts/page_classes/profile_edit_page/EditPageMainInfoSectionData";
    import MainInfoSectionLeftSide from "./main_info_section_components/MainInfoSectionLeftSide.svelte";
    import MainInfoSectionRightSide from "./main_info_section_components/MainInfoSectionRightSide.svelte";
    import AboutMeUpdateDialog from "./main_info_section_components/left_side_components/AboutMeUpdateDialog.svelte";
    import BannerColorUpdateDialog from "./main_info_section_components/left_side_components/BannerColorUpdateDialog.svelte";
    import BirthdateUpdateDialog from "./main_info_section_components/left_side_components/BirthdateUpdateDialog.svelte";
    import RealNameUpdateDialog from "./main_info_section_components/left_side_components/RealNameUpdateDialog.svelte";
    import UsernameUpdateDialog from "./main_info_section_components/left_side_components/UsernameUpdateDialog.svelte";

    export let sectionData: EditPageMainInfoSectionData;
    let usernameUpdatingDialog: UsernameUpdateDialog;
    let bannerColorUpdatingDialog: BannerColorUpdateDialog;
    let realNameUpdatingDialog: RealNameUpdateDialog;
    let birthDateUpdatingDialog: BirthdateUpdateDialog;
    let aboutMeUpdatingDialog: AboutMeUpdateDialog;
</script>

<div class="main-info-section">
    <UsernameUpdateDialog
        bind:this={usernameUpdatingDialog}
        currentUsername={sectionData.username}
        updateParentElement={(newValue) => (sectionData.username = newValue)}
    />
    <BannerColorUpdateDialog
        bind:this={bannerColorUpdatingDialog}
        currentBannerColor={sectionData.bannerColor}
        updateParentElement={(newValue) => (sectionData.bannerColor = newValue)}
    />
    <RealNameUpdateDialog
        bind:this={realNameUpdatingDialog}
        currentName={sectionData.realName}
        updateParentElement={(newVal) => (sectionData.realName = newVal)}
    />
    <BirthdateUpdateDialog
        bind:this={birthDateUpdatingDialog}
        updateParentElement={(newVal) => (sectionData.birthDate = newVal)}
    />
    <AboutMeUpdateDialog
        bind:this={aboutMeUpdatingDialog}
        currentAboutMe={sectionData.aboutMe}
        updateParentElement={(newValue) => (sectionData.aboutMe = newValue)}
    />
    <MainInfoSectionLeftSide
        openUsernameEditingDialog={() => usernameUpdatingDialog.open()}
        openBannerColorEditingDialog={() => bannerColorUpdatingDialog.open()}
        openRealNameEditingDialog={() => realNameUpdatingDialog.open()}
        openBirthDateEditingDialog={() => birthDateUpdatingDialog.open()}
        openAboutMeEditingDialog={() => aboutMeUpdatingDialog.open()}
        username={sectionData.username}
        bannerColor={sectionData.bannerColor}
        realName={sectionData.realName}
        birthDate={sectionData.birthDate}
        registrationDate={sectionData.registrationDate}
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
