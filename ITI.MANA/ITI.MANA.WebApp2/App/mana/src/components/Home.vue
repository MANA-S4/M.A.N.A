<template>
  <div class="container">
    <h1>M.A.N.A</h1>
    <h2>Notre application vous aidera à organiser votre vie familiale ou entre colocataire. Vous pourrez ajouter des contacts,
      gérer vos calendriers (et inviter votre famille ou vos coloc' à des évènements). De plus vous pourrez ajouter des tâches
      pour savoir ce qu'il vous reste à faire.</h2>

    <div class="container">

      <hr class="featurette-divider">

      <div class="featurette" id=calendars>
        <img class="featurette-image img-circle img-responsive pull-right" src="../pictures/Calendar.png">
        <h2 class="featurette-heading">Des calendriers,
          <span class="text-muted">pour planifier.</span>
        </h2>
        <p class="lead">M.A.N.A vous propose d'importer votre calendier Google pour pouvoir visualiser tout vos rendez-vous. Il vous propose
          également un calendrier local qui va vous permettre d'ajouter, de modifier ou de supprimer un évènement. Vous pouvez
          faire participer votre famille ou vos colocataires dans vos évènements afin de partager ces moments ensemble
          !</p>
      </div>

      <hr class="featurette-divider">

      <div class="featurette" id="tasks">
        <img class="featurette-image img-circle img-responsive pull-left" src="../pictures/List.png">
        <h2 class="featurette-heading">Des tâches,
          <span class="text-muted">pour attribuer.</span>
        </h2>
        <p class="lead">Les tâches vont vous permettre d'organiser ce que vous avez de plus urgent à faire. Faire les courses ? Faire le
          ménage ? Vous pourrez suivre l'avancement de toutes vos tâches ! En plus, vous pouvez dire à votre famille ou à
          vos colocataires que vous en avez marre de toujours faire le ménage et l'attribuer à une personne pour enfin changer
          les règles... Qui n'existe que pour eux.</p>
      </div>

      <hr class="featurette-divider">

      <div class="featurette" id="contacts">
        <img class="featurette-image img-circle img-responsive pull-right" src="../pictures/Contact.png">
        <h2 class="featurette-heading">Des contacts,
          <span class="text-muted">pour être ensemble.</span>
        </h2>
        <p class="lead">Vous voulez changer les choses ? Que votre famille ou vos colocataires soient moins laxiste ? En ajoutant des contacts
          sur M.A.N.A une invitation leur est envoyé afin que tout le monde puisse échanger et participer ensemble.</p>
      </div>

      <hr class="featurette-divider">

      <footer>
        <div class="row">
          <div class="col-lg-12">
            <p>Application réalisée par Julie L et Yacine C - Semestre 4 IL</p>
          </div>
        </div>
      </footer>

    </div>
  </div>
</template>

<script>
  export default {

  }
</script>

<style lang="less">
h1 {
  font-size: 35px;
}

.text-muted {
  color: darkorange;
}

footer {
  font-style: oblique;
}
</style>