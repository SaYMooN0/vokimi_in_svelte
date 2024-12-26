<script lang="ts">
    import type { ManageTestConclusionTabData } from "../../../ts/page_classes/manage_test_page/conclusion/ManageTestConclusionTabData";

    export let testId: string;
    export let tabConclusionData: ManageTestConclusionTabData;
</script>

<div class="conclusion">
    <p>{tabConclusionData}</p>
</div>
