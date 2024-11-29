<script lang="ts">
    import { Err } from "../../../ts/Err";
    import type { EditPageMainInfoSectionData } from "../../../ts/page_classes/profile_edit_page/EditPageMainInfoSectionData";
    import EditButton from "../section_shared_components/EditButton.svelte";
    import MainInfoSectionLeftSide from "./main_info_section_components/MainInfoSectionLeftSide.svelte";
    import MainInfoSectionRightSide from "./main_info_section_components/MainInfoSectionRightSide.svelte";

    export let sectionData: EditPageMainInfoSectionData;
    export let openTextFieldEditingDialog: (
        dialogTitleMessage: string,
        initialValue: string,
        inputPlaceholder: string,
        saveChangesFunc: (newVal: string) => Promise<Err>,
    ) => void;

    function openUsernameEditingDialog() {
        openTextFieldEditingDialog(
            "Input new username",
            sectionData.username,
            "Username",
            async (newVal: string) => {
                return new Err("Not implemented");
            },
        );
    }
</script>

<div class="main-info-section">
    <MainInfoSectionLeftSide
        {openUsernameEditingDialog}
        username={sectionData.username}
        bannerColor={sectionData.bannerColor}
        aboutMe={sectionData.aboutMe}
    />
    <MainInfoSectionRightSide profilePicPath={sectionData.profilePicture} />
</div>

<style>
    .main-info-section {
        width: 100%;
        display: grid;
        grid-template-columns: 1fr auto;
    }
</style>
