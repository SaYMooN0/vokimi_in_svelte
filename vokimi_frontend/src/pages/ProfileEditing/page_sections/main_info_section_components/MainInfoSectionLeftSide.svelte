<script lang="ts">
    import EditButton from "../../section_shared_components/EditButton.svelte";

    export let openUsernameEditingDialog: () => void;
    export let openAboutMeEditingDialog: () => void;
    export let openBannerColorEditingDialog: () => void;

    export let username: string;
    export let bannerColor: string;
    export let realName: string;
    export let birthDate: Date | null;
    export let aboutMe: string;
    export let registrationDate: Date;
</script>

<div class="section-left-side">
    <p class="editable-field-p">
        <span class="unselectable">My Username:</span>
        <label class="username-label">
            {username}
        </label>
        <EditButton
            showText={false}
            editButtonAction={openUsernameEditingDialog}
        />
    </p>
    <div class="editable-field-p">
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
        <EditButton
            showText={false}
            editButtonAction={openBannerColorEditingDialog}
        />
    </div>
    <div class="editable-field-p">
        <span>real name</span>
        <EditButton showText={false} editButtonAction={() => {}} />
    </div>
    <div class="editable-field-p">
        <span>birthDate</span>
        <EditButton showText={false} editButtonAction={() => {}} />
    </div>
    <div>
        <span>registrationDate</span>
    </div>
    <div class="editable-field-p">
        <span>About me</span>
        <label>{aboutMe || "No description"}</label>
        <EditButton
            showText={false}
            editButtonAction={openAboutMeEditingDialog}
        />
    </div>
</div>

<style>
    .section-left-side {
        display: flex;
        flex-direction: column;
    }
    .editable-field-p {
        min-width: 280px;
        max-width: 640px;
        position: relative;
        margin: 12px 12px 0 0;
        padding: 14px 0 0 0;
        display: flex;
        justify-content: space-between;
        align-items: start;
    }
    .editable-field-p span {
        position: absolute;
        top: 0px;
        left: 12px;
        color: var(--text-faded);
        font-size: 14px;
    }
    .username-label {
        color: var(--text);
        font-size: 28px;
        max-width: inherit;
        overflow: hidden;
        word-break: break-all;
        line-height: 24px;
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
</style>
