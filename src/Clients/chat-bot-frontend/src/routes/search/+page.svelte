<script lang="ts">
    import QuestionList from "../../components/QuestionList/QuestionList.svelte";
    import Loader from "../../components/Loader/Loader.svelte";

    export let data: { searchTerm: string, questions: QuestionListElement[] }

    let searchTerm = data.searchTerm;
    let questions = data.questions;

    $: searchTerm = data.searchTerm;
    $: questions = data.questions;

    $: title = `Searching for ${searchTerm}`;

</script>

<div class="shadow rounded-lg p-4 max-w-5xl mx-auto">
    <a href="javascript: history.go(-1)" class="flex items-center  w-1/2 py-2
        transition-colors duration-200 rounded-lg gap-x-2 sm:w-auto d
        ark:hover:bg-gray-800 mb-8">
        <svg width="24" height="24" viewBox="0 0 24 24" fill="none" xmlns="http://www.w3.org/2000/svg">
            <path d="M12 18L6 12L12 6" stroke="#B6CCF7" stroke-width="2"/>
            <path d="M18 18L12 12L18 6" stroke="#B6CCF7" stroke-width="2"/>
        </svg>
    </a>
    {#if questions === undefined}
        <Loader/>
    {:else}
        <QuestionList {title} {questions}/>
    {/if}
</div>