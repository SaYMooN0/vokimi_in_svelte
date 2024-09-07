<script lang="ts">
    export let isChecked: boolean = false;
</script>

<div class="custom-checkbox">
    <input type="checkbox" bind:checked={isChecked} />
    <span class="checkmark" />
</div>

<style>
</style>
