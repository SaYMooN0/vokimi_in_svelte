<script lang="ts">
    import { StringUtils } from "../../../../ts/utils/StringUtils";
    import EditButton from "../../section_shared_components/EditButtonWithIcon.svelte";

    export let openUsernameEditingDialog: () => void;
    export let openBannerColorEditingDialog: () => void;
    export let openRealNameEditingDialog: () => void;
    export let openBirthDateEditingDialog: () => void;
    export let openAboutMeEditingDialog: () => void;

    export let username: string;
    export let bannerColor: string;
    export let realName: string;
    export let birthDate: Date | null;
    export let aboutMe: string;
    export let registrationDate: Date;
</script>

<div class="section-left-side">
    <p class="editable-field-div">
        <span class="unselectable">Username:</span>
        <label class="field-value">
            {username}
        </label>
        <EditButton editButtonAction={openUsernameEditingDialog} />
    </p>
    <div class="editable-field-div">
        <span class="unselectable">Page Banner Color:</span>
        <div class="banner-color-field">
            <label>
                {bannerColor}
            </label>
            <div
                class="banner-color-view-div"
                style="background-color: {bannerColor}"
            />
        </div>
        <EditButton editButtonAction={openBannerColorEditingDialog} />
    </div>
    <div class="editable-field-div">
        <span class="unselectable">Real name:</span>
        {#if StringUtils.isNullOrWhiteSpace(realName)}
            <label class="field-val-not-set">(Not set)</label>
        {:else}
            <label class="field-value">{realName}</label>
        {/if}
        <EditButton editButtonAction={openRealNameEditingDialog} />
    </div>
    <div class="editable-field-div">
        <span class="unselectable">Birthdate:</span>
        {#if birthDate === null}
            <label class="field-val-not-set">(Not set)</label>
        {:else}
            <label class="field-value">{birthDate.toLocaleString()}</label>
        {/if}
        <EditButton editButtonAction={openBirthDateEditingDialog} />
    </div>
    <div class="editable-field-div">
        <span class="unselectable">Registration date:</span>
        <label class="field-value">
            {registrationDate.toLocaleString()}
        </label>
    </div>
    <div class="editable-field-div">
        <span class="unselectable">About me:</span>
        {#if StringUtils.isNullOrWhiteSpace(aboutMe)}
            <label class="field-val-not-set">(Not set)</label>
        {:else}
            <label class="about-me-text">{aboutMe}</label>
        {/if}
        <EditButton editButtonAction={openAboutMeEditingDialog} />
    </div>
</div>

<style>
    .section-left-side {
        display: flex;
        flex-direction: column;
    }
    .editable-field-div {
        min-width: 280px;
        max-width: 640px;
        position: relative;
        margin: 12px 12px 0 0;
        padding: 14px 0 0 0;
        display: flex;
        justify-content: space-between;
        align-items: start;
    }
    .editable-field-div span {
        position: absolute;
        top: 0px;
        left: 12px;
        color: var(--text-faded);
        font-size: 14px;
    }
    .field-value {
        margin: 0;
        color: var(--text);
        font-size: 28px;
        max-width: inherit;
        overflow: hidden;
        word-break: break-all;
        align-self: flex-end;
    }
    .field-val-not-set {
        font-size: 24px;
    }
    .banner-color-field {
        display: grid;
        grid-template-columns: 1fr 1fr;
        align-items: center;
        gap: 12px;
    }
    .banner-color-field label {
        color: var(--text);
        font-size: 24px;
    }
    .banner-color-view-div {
        height: 90%;
        width: 100%;
        border-radius: 5px;
    }
    .about-me-text {
        color: var(--text);
        font-size: 24px;
    }
</style>
