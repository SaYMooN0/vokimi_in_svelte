<script lang="ts">
    import { ImgUtils } from "../../../ts/utils/ImgUtils";

    export let pictureUrl: string = "";
    export let username: string = "";
</script>

<div class="user-info">
    <div class="profile-picture">
        <img src={ImgUtils.imgUrl(pictureUrl)} alt="Profile Picture" />
    </div>
</div>

<style>
    .profile-picture {
    }
    .profile-picture img {
    }
</style>
