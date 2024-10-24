<script lang="ts">
    import type { TestCreatorBasicData } from "../../../ts/view_test_page_classes/TestCreatorBasicData";
    import TestCreatorViewComponent from "./TestCreatorViewComponent.svelte";

    export let creatorData: TestCreatorBasicData;
</script>

<p>Following required</p>
<TestCreatorViewComponent {creatorData} />
