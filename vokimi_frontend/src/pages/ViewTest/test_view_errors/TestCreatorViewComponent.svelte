<script lang="ts">
    import type { TestCreatorBasicData } from "../../../ts/view_test_page_classes/TestCreatorBasicData";

    export let creatorData: TestCreatorBasicData;

    function follow() {
        //send requests
        //reload page
    }
</script>

<p>{JSON.stringify(creatorData)}</p>
<button>Follow</button>
