<script lang="ts">
    export let testName: string = "";
    export let creatorId: string = "";
    export let creatorUsername: string = "";
</script>

<div class="test-name">{testName}</div>
<label class="creatot-link">
    by <a href="/user/{creatorId}">={creatorUsername}</a>
</label>
