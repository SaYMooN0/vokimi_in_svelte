<script lang="ts">
    import { ImgUtils } from "../../../../ts/ImgUtils";

    export let imgPath: string;
</script>

<div class="img-container">
    <img src={ImgUtils.imgUrlWithVersion(imgPath)} alt="test cover" />
    <div class="img-operations-btn change-img-btn">Change Test Cover</div>
    <div class="img-operations-btn remove-img-btn">Set Cover To Default</div>
</div>

<style>
    .img-container {
        width: 600px;
        display: flex;
        flex-direction: column;
        align-items: center;
    }
</style>
