<script>
    import Question from "../../../components/Question/Question.svelte";

    export let data;

    const question = data.question;

</script>

<Question {question}/>


