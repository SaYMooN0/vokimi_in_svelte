<script lang="ts">
    import { ImgUtils } from "../../../ts/utils/ImgUtils";

    export let pictureUrl: string = "";
    export let username: string = "";
    export let userColor: string = "";
</script>

<div class="user-info">
    <div class="user-color" style="background-color:{userColor};">
        <slot name="user-color-slot" />
    </div>
    <div class="info">
        <div class="profile-picture">
            <img src={ImgUtils.imgUrl(pictureUrl)} alt="Profile Picture" />
        </div>
    </div>
</div>

<style>
    .user-info {
        display: grid;
        grid-template-columns: 160px 200px;
    }
    .user-color {
        width: 100%;
        height: 100%;
        position: relative;
        background-color: darkblue;
    }
    .profile-picture {
    }
    .profile-picture img {
    }
</style>
