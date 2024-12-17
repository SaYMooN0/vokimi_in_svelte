<script lang="ts">
    import { ImgUtils } from "../../../../ts/utils/ImgUtils";
    import ProfilePicFullSizeDialog from "./ProfilePicFullSizeDialog.svelte";

    export let profilePicture: string;
    let picFullSizeDialog: ProfilePicFullSizeDialog;
</script>

<ProfilePicFullSizeDialog bind:this={picFullSizeDialog} {profilePicture} />
<div class="profile-picture">
    <img src={ImgUtils.imgUrl(profilePicture)} alt="Profile Pic" />
    <button class="zoom-button" on:click={() => picFullSizeDialog.open()}>
        <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="none">
            <path
                d="M19.4 19.4L22 22M20.7 14.85C20.7 11.6191 18.0809 9 14.85 9C11.6191 9 9 11.6191 9 14.85C9 18.0809 11.6191 20.7 14.85 20.7C18.0809 20.7 20.7 18.0809 20.7 14.85Z"
                stroke="currentColor"
                stroke-width="1.5"
                stroke-linecap="round"
                stroke-linejoin="round"
            />
            <path
                d="M19.075 19.075L22 22M12.9 14.85H14.85M14.85 14.85H16.8M14.85 14.85V12.9M14.85 14.85V16.8M20.7 14.85C20.7 11.6191 18.0809 9 14.85 9C11.6191 9 9 11.6191 9 14.85C9 18.0809 11.6191 20.7 14.85 20.7C18.0809 20.7 20.7 18.0809 20.7 14.85Z"
                stroke="currentColor"
                stroke-width="1.5"
                stroke-linecap="round"
                stroke-linejoin="round"
            />
            <path
                d="M2 6C2.1305 4.6645 2.4262 3.7663 3.09625 3.09625C3.7663 2.4262 4.6645 2.1305 6 2M6 22C4.6645 21.8695 3.7663 21.5738 3.09625 20.9037C2.4262 20.2337 2.1305 19.3355 2 18M22 6C21.8695 4.6645 21.5738 3.7663 20.9037 3.09625C20.2337 2.4262 19.3355 2.1305 18 2M2 10L2 14M14 2L10 2"
                stroke="currentColor"
                stroke-width="1.5"
                stroke-linecap="round"
            />
        </svg>
    </button>
</div>

<style>
    .profile-picture {
        z-index: 4;
        margin-top: -50%;
        width: 100%;
        aspect-ratio: 1 / 1;
        border-radius: 50%;
        overflow: hidden;
        border: 3px solid var(--banner-color);
        background-color: var(--back-main);
        align-content: center;
        position: relative;
    }

    .profile-picture img {
        width: 100%;
        height: 100%;
        object-fit: cover;
    }

    .profile-picture .zoom-button {
        position: absolute;
        bottom: 8px;
        left: 50%;
        transform: translateX(-50%);
        opacity: 0;
        transition: all 0.1s ease-in;
        background-color: var(--back-main);
        border: none;
        outline: none;
        padding: 4px;
        height: 32px;
        width: 32px;
        box-sizing: border-box;
        border-radius: 8px;
        cursor: pointer;
    }
    .profile-picture:hover .zoom-button {
        opacity: 0.8;
    }
    .zoom-button:hover {
        opacity: 1 !important;
    }
    .zoom-button:active {
        transform: translateX(-50%) scale(0.96);
    }
    .zoom-button svg {
        height: 100%;
        width: 100%;
        color: var(--text-faded);
    }
    .zoom-button:hover svg {
        color: var(--primary);
    }
</style>
