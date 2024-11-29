<script lang="ts">
    import EditButton from "../../section_shared_components/EditButton.svelte";
    export let openUsernameEditingDialog: () => void;
    export let username: string;
    export let bannerColor: string;
    export let aboutMe: string;
</script>

<div class="section-left-side">
    <p class="username-field">
        <span>My Username:</span>
        <label>
            {username}
        </label>
        <EditButton
            showText={false}
            editButtonAction={openUsernameEditingDialog}
        />
    </p>
    <div class="banner-color-field">
        <span>Page Banner Color:</span>
        <div>
            <span>
                {bannerColor}
            </span>
            <div
                class="banner-color-view-div"
                style="background-color: {bannerColor}"
            />
        </div>
        <EditButton showText={false} editButtonAction={() => {}} />
    </div>
    <div class="about-me-field">
        <span>About me</span>
        <EditButton showText={false} editButtonAction={() => {}} />
        <p>{aboutMe}</p>
    </div>
</div>
