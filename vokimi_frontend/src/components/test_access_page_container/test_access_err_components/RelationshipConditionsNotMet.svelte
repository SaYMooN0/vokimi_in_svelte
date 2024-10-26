<script lang="ts">
    import type { TestCreatorBasicData } from "../../../ts/page_classes/view_test_page_classes/TestCreatorBasicData";

    export let creatorData: TestCreatorBasicData;
    export let conditionMessage: string;
    function follow() {
        //send requests
        //reload page
    }
</script>

<div class="relationship-conditions-not-met">
    <p class="condition-message">{conditionMessage}</p>
    <p>{JSON.stringify(creatorData)}</p>
    <button>Follow</button>
</div>
