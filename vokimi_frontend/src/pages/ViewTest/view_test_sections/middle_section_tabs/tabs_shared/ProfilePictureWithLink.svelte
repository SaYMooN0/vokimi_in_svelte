<script lang="ts">
    import { ImgUtils } from "../../../../../ts/utils/ImgUtils";
    export let profilePicturePath: string;
    export let userId: string;
</script>

<a href="/user/{userId}" class="profile-picture-link">
    <img
        src={ImgUtils.imgUrl(profilePicturePath)}
        alt="Profile Picture"
        class="profile-picture"
    />
</a>

<style>
    .profile-picture-link {
        width: 48px;
        height: 48px;
        aspect-ratio: 1/1;
        border-radius: 50%;
        display: flex;
        justify-content: center;
        align-items: center;
        box-shadow:
            rgba(0, 0, 0, 0.05) 0px 4px 16px 0px,
            rgb(67, 58, 178, 0.1) 0px 0px 0px 1px;
        transition: all 0.08s ease;
    }
    .profile-picture-link > img {
        width: 100%;
        height: 100%;
        object-fit: cover;
        border-radius: inherit;
    }
    .profile-picture-link:hover {
        border-radius: 46%;
        box-shadow:
            rgba(0, 0, 0, 0.05) 0px 4px 24px 0px,
            rgb(67, 58, 178, 0.1) 0px 0px 6px 2px;
    }
</style>
