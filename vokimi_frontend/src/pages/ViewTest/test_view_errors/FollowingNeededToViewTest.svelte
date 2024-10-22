<script lang="ts">
    import TestCreatorViewComponent from "./TestCreatorViewComponent.svelte";

    export let creatorId: string = "";
    export let creatorProfilePath: string = "";
</script>

<p>Following required</p>
<TestCreatorViewComponent {creatorId} {creatorProfilePath} />
