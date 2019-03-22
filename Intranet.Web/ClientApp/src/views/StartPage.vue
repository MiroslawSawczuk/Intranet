<template>
  <div class="container main-container">
    <div class="row">
      <div class="col-md-5">
        <span>Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.</span>
      </div>
      <div class="col-md-2"></div>
      <div class="col-md-5">
        <span>To use an intranet you must be logged</span>
        <br/>
        <br/>
        <button class="btn btn-primary" type="button">
          <a href="/api/account/sign-in">Use Microsoft account</a>
        </button>
        </div>
    </div>
  </div>
</template>

<script>

export default {
  name: 'StartPage',

 
}

</script>

<style lang="scss">
body {
  background-color: #ececec;
  
  .main-container {
    font-weight: bold;
    padding: 1em;
    margin-top: 5rem;

    a {
      text-decoration: none;
      color:#fff;
    }

    button {
      margin-left: 3rem;
    }
  }
}

</style>
