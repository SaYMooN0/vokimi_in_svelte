<script lang="ts">
    import { ImgUtils } from "../../../../ts/utils/ImgUtils";

    export let profilePicPath: string;
    let errorString: string = "";
    async function handleProfilePicInputChange() {}
    async function changeProfilePicToDefault() {}
</script>

<div class="section-right-side">
    <img
        src={ImgUtils.imgUrlWithVersion(profilePicPath)}
        alt="profile picture"
        class="profile-pic"
    />
    <input
        type="file"
        id="profile-pic-input"
        accept=".jpg,.png,.webp"
        hidden
        on:change={handleProfilePicInputChange}
    />
    <label for="profile-pic-input" class="img-operations-btn change-img-btn">
        Change Profile Picture
    </label>
    <label
        class="img-operations-btn remove-img-btn"
        on:click={changeProfilePicToDefault}
    >
        Set Default Picture
    </label>

    <span class="error-string">{errorString}</span>
</div>

<style>
    .section-right-side {
        display: flex;
        flex-direction: column;
        align-items: center;
        width: fit-content;
    }
    .profile-pic {
        max-width: 420px;
        min-width: 240px;
        max-height: 560px;
        min-height: 100px;
        width: 100%;
        height: 100%;
        object-fit: contain;
    }
    .img-operations-btn {
        width: 100%;
        display: flex;
        justify-content: center;
        align-items: center;
        background-color: var(--primary);
    }
</style>
