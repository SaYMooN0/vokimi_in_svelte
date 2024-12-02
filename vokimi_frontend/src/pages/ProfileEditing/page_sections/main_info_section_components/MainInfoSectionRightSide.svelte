<script lang="ts">
    import { getErrorFromResponse } from "../../../../ts/ErrorResponse";
    import { ImgUtils } from "../../../../ts/utils/ImgUtils";

    export let profilePicPath: string;
    let errorString: string = "";

    async function handleProfilePicInputChange(event: Event) {
        const input = event.target as HTMLInputElement;

        if (!input.files || input.files.length <= 0) {
            return;
        }
        const file = input.files[0];
        const formData = new FormData();
        formData.append("file", file);

        const response = await fetch(`/api/saveimg/updateProfilePic`, {
            method: "POST",
            body: formData,
        });

        if (response.ok) {
            const data = await response.json();
            profilePicPath = "";
            profilePicPath = data.imgPath;
        } else if (response.status === 400) {
            errorString = await getErrorFromResponse(response);
        } else {
            errorString = "Unable to update profile picture";
        }
    }

    async function changeProfilePicToDefault() {
        errorString = "";
        const response = await fetch(`/api/saveimg/setProfilePicToDefault`, {
            method: "POST",
        });
        if (response.ok) {
            const data = await response.json();
            profilePicPath = data.imgPath;
        } else if (response.status === 400) {
            errorString = await getErrorFromResponse(response);
        } else {
            errorString = "Unable to set profile picture to default";
        }
    }
</script>

<div class="section-right-side unselectable">
    <div class="img-container">
        <img
            src={ImgUtils.imgUrlWithVersion(profilePicPath)}
            alt="profile picture"
            class="profile-pic"
        />
    </div>
    <input
        type="file"
        id="profile-pic-input"
        accept=".jpg,.png,.webp"
        hidden
        on:change={handleProfilePicInputChange}
    />
    <label
        for="profile-pic-input"
        class="img-operations-btn change-img-btn"
        on:click={() => (errorString = "")}
    >
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
        width: 420px;
        display: flex;
        flex-direction: column;
        align-items: center;
        width: fit-content;
    }
    .img-container {
        min-height: 240px;
        width: 100%;
        display: flex;
        align-items: center;
        justify-content: center;
    }
    .profile-pic {
        max-width: 380px;
        min-width: 240px;
        max-height: 420px;
        min-height: 100px;
        width: 100%;
        height: 100%;
        object-fit: contain;
        border-radius: 12px;
    }
    .profile-pic {
        box-shadow:
            rgba(0, 0, 0, 0.05) 0px 6px 24px 0px,
            rgb(67, 58, 178, 0.1) 0px 0px 0px 1px;
    }
    .img-operations-btn {
        margin-top: 8px;
        max-width: 320px;
        width: 320px;
        display: flex;
        justify-content: center;
        align-items: center;
        padding: 4px 12px;
        box-sizing: border-box;
        border-radius: 6px;
        font-size: 18px;
        color: var(--back-main);
        transition: transform 0.06s ease-in;
        cursor: pointer;
    }
    .change-img-btn {
        background-color: var(--primary);
    }
    .remove-img-btn {
        background-color: var(--text-faded);
    }
    .img-operations-btn:hover {
        transform: scale(1.04);
    }
    .error-string {
        margin-top: 8px;
        color: var(--red-del);
        font-weight: 500;
    }
</style>
