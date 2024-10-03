<script lang="ts">
    import {
        TestStylesArrowType,
        TestStylesArrowTypeUtils,
    } from "../../../../../ts/enums/TestStylesArrowType";

    export let chosenType: TestStylesArrowType = TestStylesArrowType.Default;

    function chooseIconType(type: TestStylesArrowType) {
        chosenType = type;
    }
</script>

<div class="arrow-picker">
    <div class="type-choosing-container">
        {#each Object.values(TestStylesArrowType) as arrowType}
            <div
                class="arrow-type-card {arrowType === chosenType
                    ? 'chosen'
                    : ''}"
                on:click={() => chooseIconType(arrowType)}
            >
                <div class="arrows-container">
                    <!-- Отображаем иконки стрелок -->
                    <svelte:component
                        this={TestStylesArrowTypeUtils.getIcon(arrowType)}
                    />
                    <svelte:component
                        this={TestStylesArrowTypeUtils.getIcon(arrowType)}
                    />
                </div>
                <span class="arrow-name"
                    >{TestStylesArrowTypeUtils.getFullName(arrowType)}</span
                >
            </div>
        {/each}
    </div>
</div>

<style>
    .arrow-picker {
        display: flex;
        flex-direction: column;
        align-items: center;
    }
</style>
